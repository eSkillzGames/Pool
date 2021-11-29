using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class ERC721BalanceOfExample : MonoBehaviour
{
    //public Text myText;
    public Text myText2;
    async void Start()
    {
        //string chain = "ethereum";
        //string network = "ropsten";
        string contract = "0x4d040491c71c45aea79a8ae6e8fd8bb0c5a932ec";
        string account = PlayerPrefs.GetString("Account");
        string tokenId = "1";
        //string fromAddress = "0xaE1230b3d256CdB131020CFC68eC962f07dCB897";
        string toAddress = "0x0374805cE1Ffc8951265E4f4A1301abdA55314A3";

        // // int balance = await ERC721.BalanceOf(chain, network, contract, account);
        // string owner = await ERC721.OwnerOf(chain, network, contract, tokenId2);
        //int total = await ERC721.Total(chain, network, contract);
        string res = await ERC721.TransferTo(contract,toAddress, account, tokenId);
        myText2.text = res;
        //print(balance);
        // if(balance>0){
        //     // myText.text = balance.ToString();
        //     myText.text = owner;
        //     myText2.text = res;
        // }
        // else{
        //     myText.text = "NFT Not Detected";
        // }
        
    
    }
}
