using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using System.Threading.Tasks;
using Unity.VisualScripting;
using System;

public class Sdk : MonoBehaviour
{

    private async void Start()
    {
        var x = await HttpManager.HttpGet(HttpManager.BuildUrl("/api/v1/get-all-nfts"));
         Gamer a = await  SignIn("0x12345");
        int quantity = await Verify("0x12345","631b32532151223c8e1e3a0a");
        var b= await getAllNfts();
        var v = await getAllNftsByGamer("0x12345");
        print(v[0].name);
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
        var response = await HttpManager.HttpGet(HttpManager.BuildUrl( $"/api/v1/verify/{walletAddress}/{nftId}"));
        return Int32.Parse(response);
    }

     public async Task<List<Nft>> getAllNfts()
    {
        var response = await HttpManager.HttpGet(HttpManager.BuildUrl( "/api/v1/get-all-nfts"));
        var nftsJson = JsonConvert.DeserializeObject<List<Nft>>(response);
        return nftsJson;
    }

      public async Task<List<Nft>> getAllNftsByGamer(string walletAddress)
    {
        var response = await HttpManager.HttpGet(HttpManager.BuildUrl($"/api/v1/get-all-nfts/{walletAddress}"));
        var nftsJson = JsonConvert.DeserializeObject<List<Nft>>(response);
        return nftsJson;
    }

}