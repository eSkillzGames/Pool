using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
public class Mint : MonoBehaviour
{
    public Text resultText;
    public Text AddressText;
    private string contract = "";
    public InputField inputField;
    private string res;
    
    void Start()
    {
        string account = PlayerPrefs.GetString("Account");
        if (account != "")
        {
            AddressText.text = account.Substring(0, 12) + "..." + account.Substring(account.Length - 3, 3);
            resultText.text = "";
            contract = PlayerPrefs.GetString("Contract");
        }
        else
        {
            resultText.text = "You have to connect Wallet.";
        }
        
        //contract = "0x483869B846ED1541cA726f913b99EC87D0b92351";
    }
    void Update(){
        //if(res!=""){
        //    resultText.text = "Token Hash: "+res;
        //}
    }
    async public void Mint_To() { 
        string account = PlayerPrefs.GetString("Account");
        if (account != "")
        {
            string metadataUri = inputField.text;
            string res = await ERC721.MintTo(contract, account, metadataUri);
            if (res.Length>0)
            {
                resultText.text = "Token Hash: " + res;
            }
        }
        else
        {
            resultText.text = "You have to connect Wallet.";
        }
          
    }

}

