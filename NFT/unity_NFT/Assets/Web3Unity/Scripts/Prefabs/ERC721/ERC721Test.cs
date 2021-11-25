using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using UnityEngine.Networking;

public class ERC721Test : MonoBehaviour
{
    public Text myText;
    public Text myText3;
    public Text myText2;
    private string url = "https://gateway.pinata.cloud/ipfs/QmeJUgNoMXG8aNg6rdS61wo6aR2fG6pP9QPsakT6VT9S11";
    private WWW www = null;
    public string jsontxt = "";
    public string[,] metadata= new string[100,3];
    public int metaDataIndex = 0;
    async void Start()
    { 
        string chain = "ethereum";
        string network = "ropsten";
        string contract = "0x483869B846ED1541cA726f913b99EC87D0b92351";
        string account = PlayerPrefs.GetString("Account");
        //string tokenId = "1";
        //string fromAddress = "0xaE1230b3d256CdB131020CFC68eC962f07dCB897";
        //string toAddress = "0x0374805cE1Ffc8951265E4f4A1301abdA55314A3";

        //string res = await ERC721.TransferTo(contract,toAddress, account, tokenId);
        //string owner = await ERC721.OwnerOf(chain, network, contract, tokenId);

        /*int total = await ERC721.Total(chain, network, contract);
        int balance = await ERC721.BalanceOf(chain, network, contract, account);

        string tokenListIDs = await ERC721.OwnedList(chain, network, contract, account);
        string subTokenListIDs = tokenListIDs.Substring(1, tokenListIDs.Length-2);
        string[] ownedIDs = subTokenListIDs.Split(',');*/
        metaDataIndex = 0;
        // for (int i = 0; i < ownedIDs.Length; i++)
        // {
        //     string ID = ownedIDs[i].Substring(1, ownedIDs[i].Length-2);
        //     string tokenURI = await ERC721.URI(chain, network, contract, ID);
        //     if(i<1){
        //         myText.text = tokenURI;
        //     }
        //     else{
        //         myText2.text = tokenURI;
        //     }
        // };

                
        www = new WWW (url);
        StartCoroutine (ReceiveResponse());
        // myText.text = total.ToString();
        // myText2.text = tokenListIds;
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
    
    private IEnumerator ReceiveResponse() {
        yield return www;  
        //jsontxt = www.text;
        string jsontxt = www.text.Substring(1, www.text.Length-2);
        string[] metaDataTxt = jsontxt.Split(',');
        for(int meta_i = 0; meta_i< metaDataTxt.Length; meta_i++){
            string[] metaDataTxtCol = metaDataTxt[meta_i].Split(':');
            string metadataName = "";
            string metadataValue = "";
            if(metaDataTxtCol.Length==2){
                metadataName = metaDataTxtCol[0].Substring(1, metaDataTxtCol[0].Length-2);
                metadataValue = metaDataTxtCol[1].Substring(1, metaDataTxtCol[1].Length-2);
            }
            else if(metaDataTxtCol.Length>2){
                metadataName = metaDataTxtCol[0].Substring(1, metaDataTxtCol[0].Length-2);
                metadataValue = metaDataTxt[meta_i].Substring(metaDataTxtCol[0].Length+2,metaDataTxt[meta_i].Length-(metaDataTxtCol[0].Length+3));
            }
            
            if(metadataName=="name"){
                metadata[metaDataIndex,0] = metadataValue;
            }
            else if(metadataName=="description"){
                metadata[metaDataIndex,1] = metadataValue;
            }
            else if(metadataName=="image"){
                metadata[metaDataIndex,2] = metadataValue;
            }
        }
        myText.text = metadata[metaDataIndex,0];
        myText2.text = metadata[metaDataIndex,1];
        myText3.text = metadata[metaDataIndex,2];
        metaDataIndex++;
    }
    
}
