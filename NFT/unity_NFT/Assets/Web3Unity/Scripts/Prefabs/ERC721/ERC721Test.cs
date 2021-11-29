using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using UnityEngine.Networking;
using System.Runtime.InteropServices;

[System.Serializable]
public class myJson
{
    public string name;
    public string image;
    public string description;
}

public class ERC721Test : MonoBehaviour
{
    public Text totaltokenText;
    public Text balanceText;
    public Text myTextName0;    
    public Text myTextDescription0;
    public Text AddressText;     
    public RawImage img0;     
    public Text TokenIDList;
    public Text TokenIDTxt;
    public InputField inputField;
    
    private string[] metadata= new string[3];
    
    private string[] ownedIDs;
    private string showId;
    private string chain = "ethereum";
    private string network = "ropsten";
    private string contract = "0x828F4EF06B8AB1785e77A21485Eaa81ad3DEFBA3";
    async void Start()
    { 
        string account = PlayerPrefs.GetString("Account");
        PlayerPrefs.SetString("Contract", contract);
        // string account = "0xaE1230b3d256CdB131020CFC68eC962f07dCB897";
        // PlayerPrefs.SetString("Account", account);
        if (account != "")
        {
            AddressText.text = account.Substring(0, 12)+"..."+account.Substring(account.Length-3, 3);
            //string account = "0xaE1230b3d256CdB131020CFC68eC962f07dCB897";
            showId = "Owned Token ID : ";

            //string owner = await ERC721.OwnerOf(chain, network, contract, tokenId);

            int total = await ERC721.Total(chain, network, contract);
            totaltokenText.text = "Total Tokens : "+total.ToString();
            int balance = await ERC721.BalanceOf(chain, network, contract, account);
            balanceText.text = "Balance of Owned Tokens : "+balance.ToString();

            string tokenListIDs = await ERC721.OwnedList(chain, network, contract, account);
            if(tokenListIDs.Length>2){
                string subTokenListIDs = tokenListIDs.Substring(1, tokenListIDs.Length-2);
                ownedIDs = subTokenListIDs.Split(',');
                
                for (int i = 0; i < ownedIDs.Length; i++)
                {
                    string ID = ownedIDs[i].Substring(1, ownedIDs[i].Length-2);
                    showId = showId+ID+", ";                 
                };

                TokenIDList.text = showId;

                if(ownedIDs.Length>0){
                    string tokenURI = await ERC721.URI(chain, network, contract, ownedIDs[0].Substring(1, ownedIDs[0].Length-2));
                    TokenIDTxt.text = "Token ID : "+ownedIDs[0].Substring(1, ownedIDs[0].Length-2);
                    StartCoroutine(SendRequest(tokenURI));
                }        
            }  
        }
                           
           
    }

    IEnumerator SendRequest(string url)
    {        
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result ==UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(string.Format("Error: {0}",request.error));
        }
        else
        {
            string buffer = request.downloadHandler.text.ToString();
            myJson P = JsonUtility.FromJson<myJson>(buffer);
            myTextName0.text = "Name : " + P.name;
            myTextDescription0.text = "Description : "+ P.description;
            DownloadImage(P.image);            
        }
    }

    public void DownloadImage(string url)
    {
        StartCoroutine(ImageRequest(url, (UnityWebRequest req) =>
        {
            if (req.result == UnityWebRequest.Result.ConnectionError|| req.result ==UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log($"{req.error}: {req.downloadHandler.text}");
            } else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(req);
                img0.texture = texture;

                //image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
        }));       
        
    }

    IEnumerator ImageRequest(string url, Action<UnityWebRequest> callback)
    {
        using (UnityWebRequest req = UnityWebRequestTexture.GetTexture(url))
        {            
            yield return req.SendWebRequest();            
            callback(req);
        }
    }

    async public void showNFT() { 
        int check = 0;
        string id_input = inputField.text.ToString();
        for (int i = 0; i < ownedIDs.Length; i++)
        {
            string ID = ownedIDs[i].Substring(1, ownedIDs[i].Length-2);
            if(ID == id_input){
                check = 1;
            }                
        };
        if(check>0){
            string tokenURI = await ERC721.URI(chain, network, contract, id_input);
            TokenIDTxt.text = "Token ID : "+id_input;
            StartCoroutine(SendRequest(tokenURI));
        }
        else{
            TokenIDTxt.text = "Token ID : "+id_input+" is not owned Token.";
        }
        
    }

}
