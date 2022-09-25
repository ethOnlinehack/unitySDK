using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Unity.VisualScripting;
using System;
using System.IO;
using UnityEngine.Networking;
using System.Collections;
using WalletConnectSharp.Core;
using WalletConnectSharp.Core.Models;
using WalletConnectSharp.Core.Models.Ethereum;
using WalletConnectSharp.Unity;

public class Sdk : WalletConnectActions
{
    Texture2D tex;
    private Sprite sp;
    public SpriteRenderer sr;

    private async void Start()
    {
        WalletConnect.ActiveSession.OnSessionConnect += ActiveSessionOnConnect;
        // var x = await HttpManager.HttpGet(HttpManager.BuildUrl("/api/v1/get-all-nfts"));
        // Gamer a = await SignIn("0x12345");
        // int quantity = await Verify("0x12345", "631b32532151223c8e1e3a0a");
        // var b = await getAllNfts();
        // var v = await getAllNftsByGamer("0x12345");

        // sr = GetComponent<SpriteRenderer>();
        // StartCoroutine(GetTexture());
        print(Directory.GetCurrentDirectory());

     //string path =  await  HttpManager.HttpImage("https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/1200px-Image_created_with_a_mobile_phone.png");
    
        StartCoroutine(LoadTexture("https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/1200px-Image_created_with_a_mobile_phone.png"));
    }

    public async void ActiveSessionOnConnect(){
        await SignIn(getAddress());
    }

    public void click(){
        Debug.Log(getAddress());
    }

    public String getAddress(){
        return WalletConnect.ActiveSession.Accounts[0];
    }

    public async Task<Gamer> SignIn(string walletAddress)
    {
        Dictionary<string, string> walletAddressJson = new Dictionary<string, string>() { { "wallet_address", walletAddress } };
        var response = await HttpManager.HttpPost(HttpManager.BuildUrl("/api/v1/gamer-sign-in"), walletAddressJson);
        print(response);
        Gamer gamer = JsonConvert.DeserializeObject<Gamer>(response);
        return gamer;
    }

    public async Task<int> Verify(string walletAddress, string nftId)
    {
        var response = await HttpManager.HttpGet(HttpManager.BuildUrl($"/api/v1/verify/{walletAddress}/{nftId}"));
        return Int32.Parse(response);
    }

    public async Task<List<Nft>> getAllNfts()
    {
        var response = await HttpManager.HttpGet(HttpManager.BuildUrl("/api/v1/get-all-nfts"));
        var nftsJson = JsonConvert.DeserializeObject<List<Nft>>(response);
        return nftsJson;
    }

    public async Task<List<Nft>> getAllNftsByGamer(string walletAddress)
    {
        var response = await HttpManager.HttpGet(HttpManager.BuildUrl($"/api/v1/get-all-nfts/{walletAddress}"));
        var nftsJson = JsonConvert.DeserializeObject<List<Nft>>(response);
        return nftsJson;
    }

    public async Task<string> mint(int tokenId){
        var address = getAddress();
        var response = await HttpManager.HttpPost(HttpManager.BuildUrl($"/api/v1/mint/{address}/{tokenId}"));
        return response;
}

    public async Task<string> transferTo(string to, int tokenId){
        var address = getAddress();
        var response = await HttpManager.HttpPost(HttpManager.BuildUrl($"/api/v1/mint/{address}/{to}/{tokenId}"));
        return response;
    }


    // Start is called before the first frame update
    


    IEnumerator LoadTexture(string url)
    {
        //test if it works in game 
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
            {
                yield return uwr.SendWebRequest();
                if (string.IsNullOrEmpty(uwr.error))
                {
                    //print( DownloadHandlerTexture.GetContent(uwr));
                    Texture2D myTexture = DownloadHandlerTexture.GetContent(uwr);                //myTexture.LoadImage(uwr.downloadHandler.data);
                    sp = Sprite.Create(myTexture, new Rect(0.0f, 0.0f, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
                    sr.sprite = sp;
                }
                else
                {
                    Debug.Log(uwr.error);
                }
            }


    }

    IEnumerator GetTexture()
    {
        //test if it works in game 

        DirectoryInfo dir = new DirectoryInfo(Application.dataPath + "/Art Work");
        FileInfo[] info = dir.GetFiles("*.png");

        foreach (FileInfo f in info)
        {
            Debug.Log(f.ToString());

            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture("file://" + f))
            {
                yield return uwr.SendWebRequest();
                if (string.IsNullOrEmpty(uwr.error))
                {
                    //print( DownloadHandlerTexture.GetContent(uwr));
                    Texture2D myTexture = DownloadHandlerTexture.GetContent(uwr);                //myTexture.LoadImage(uwr.downloadHandler.data);
                    sp = Sprite.Create(myTexture, new Rect(0.0f, 0.0f, myTexture.width, myTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
                    sr.sprite = sp;
                }
                else
                {
                    Debug.Log(uwr.error);
                }
            }


        }
    }
}