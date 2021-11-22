<template>
  <div class="bg-gray-100">
    <div class="relative">
      <transition name="fade">
        <div class="fixed inset-x-0 top-0 m-auto mt-4 flex items-center bg-red-500 text-white text-sm font-bold px-4 py-3 lg:w-2/6 rounded" role="alert" v-if="alertShow">
          <p>{{alertMsg}}</p>
        </div>
      </transition>
      <!-- Metamask and Walletconnect buttons -->
      <div class="text-right p-6 space-x-4 flex justify-end">
        <div class="relative inline-block text-left">
          <div>
            <button type="button"
                class="whitespace-nowrap inline-flex items-center px-3 py-2 border border-transparent shadow-sm text-sm leading-4 font-medium rounded-md text-gray-700 bg-white hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
                id="menu-button" aria-expanded="false" aria-haspopup="true"
                @click="dropdownShow=!dropdownShow"
                >
              <img v-if="networkId==56" class="-ml-0.5 mr-2 h-4 w-4" src="../assets/img/icons/bnb.png">
              <img v-else class="-ml-0.5 mr-2 h-4 w-4" src="../assets/img/providers/ethereum.svg">
              <span class="lg:block">Chain ID&nbsp;</span> <strong>{{this.networkId}}</strong>
              <svg v-if="!dropdownShow" class="w-5 h-5 ml-2 -mr-1" viewBox="0 0 20 20" fill="currentColor">
                <path fill-rule="evenodd"
                      d="M5.293 7.293a1 1 0 011.414 0L10 10.586l3.293-3.293a1 1 0 111.414 1.414l-4 4a1 1 0 01-1.414 0l-4-4a1 1 0 010-1.414z"
                      clip-rule="evenodd"></path>
              </svg>
              <svg v-else xmlns="http://www.w3.org/2000/svg" class="h-5 w-5  ml-2 -mr-1" viewBox="0 0 20 20" fill="currentColor">
                <path fill-rule="evenodd" d="M14.707 12.707a1 1 0 01-1.414 0L10 9.414l-3.293 3.293a1 1 0 01-1.414-1.414l4-4a1 1 0 011.414 0l4 4a1 1 0 010 1.414z" clip-rule="evenodd" />
              </svg>
            </button>
          </div>
          <div v-show="dropdownShow" class="origin-top-right absolute right-0 mt-2 w-44 rounded-md shadow-lg bg-white ring-1 ring-black ring-opacity-5 focus:outline-none" role="menu" aria-orientation="vertical" aria-labelledby="menu-button" tabindex="-1"
          >
            <div class="py-0.5" role="none">
              <button type="button"
                v-if="networkId==1"
                class="w-44 whitespace-nowrap inline-flex items-center px-3 py-2 border border-transparent shadow-sm text-sm leading-4 font-medium rounded-md text-gray-700 bg-white hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
                id="menu-button" aria-expanded="false" aria-haspopup="true"
                @click="switchNetwork('0x38')"
                >
                <img class="-ml-0.5 mr-2 h-4 w-4" src="../assets/img/icons/bnb.png">
                <span class="lg:block"><strong>BSC Mainnet</strong></span>

              </button>
              <button type="button"
                v-else
                class="whitespace-nowrap inline-flex items-center px-3 py-2 border border-transparent shadow-sm text-sm leading-4 font-medium rounded-md text-gray-700 bg-white hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
                id="menu-button" aria-expanded="false" aria-haspopup="true"
                @click="switchNetwork('0x1')"
                >
                <img class="-ml-0.5 mr-2 h-4 w-4" src="../assets/img/providers/ethereum.svg">
                <span class="lg:block"><strong>Ethereum Mainnet</strong></span>
              </button>
            </div>
          </div>
        </div>
        <a v-if="ethereum==null" href="http://www.metamask.io" target="_blank"
                class="inline-flex items-center px-3 py-2 border border-transparent shadow-sm text-sm leading-4 font-medium rounded-md text-gray-700 bg-white hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500">
          <img class="-ml-0.5 mr-2 h-4 w-4" src="../assets/img/providers/metamask.svg">
          <span class="lg:block">Install MetaMask</span>
        </a>
        <button v-else-if="!account" type="button" @click="connectMetaMask()"
                class="inline-flex items-center px-3 py-2 border border-transparent shadow-sm text-sm leading-4 font-medium rounded-md text-gray-700 bg-white hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500">
          <img class="-ml-0.5 mr-2 h-4 w-4" src="../assets/img/providers/metamask.svg">
          <span  class="lg:block">Connect with MetaMask</span>
        </button>
        <button v-else type="button"
                class="inline-flex items-center px-3 py-2 border border-transparent shadow-sm text-sm leading-4 font-medium rounded-md text-gray-700 bg-white hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500">
          <img class="-ml-0.5 mr-2 h-4 w-4" src="../assets/img/providers/metamask.svg">
          <span  class="lg:block">{{this.account.substring(0,6)+"..."+this.account.substring(this.account.length-4,this.account.length)}}</span>
        </button>
      </div>
    </div>

    <section class="container max-w-screen-lg mx-auto py-6 flex justify-center items-center">
      <img class="h-12" src="../assets/img/logo/coin.png" alt="screenshot">
      <div class="pl-3">
        <h1 class="text-center text-4xl text-black">
          CUE TOKEN SALE ADMIN PAGE</h1>
      </div>
    </section>

    <section class="bg-gradient-to-r from-blue-500 to-purple-600 py-12 lg:px-10">
      <div v-if="networkId==1||networkId==56||networkId==97" class="m-auto d-flex lg:w-2/6 bg-white p-4 rounded .shadow-2xl">
        <h2 class="block mb-4 px-6 font-bold text-left text-black">
          Configure the properties for token sale
        </h2>
        <div class="mt-2 flex flex-row items-center justify-between mb-6 px-6">
          <input class="pl-2 w-6/12 font-medium text-xl py-1 rounded border-2 focus:outline-none focus:shadow-outline" type="text" placeholder="Input Token Price" v-model="price">
          <button class="w-5/12 bg-purple-600 hover:bg-purple-700 text-white font-medium py-2 px-4 rounded focus:outline-none focus:shadow-outline" type="button"
            @click="setPrice"
          >
            Set Price
          </button>
        </div>
        <div class="mt-2 flex flex-row items-center justify-between mb-6 px-6">
          <input class="pl-2 w-6/12 font-medium text-xl py-1 rounded border-2 focus:outline-none focus:shadow-outline" type="text" placeholder="Input Minimum" v-model="minimum">
          <button class="w-5/12 bg-purple-600 hover:bg-purple-700 text-white font-medium py-2 px-4 rounded focus:outline-none focus:shadow-outline" type="button"
            @click="setAirdropAmount"
          >
            Set airdrop amount
          </button>
        </div>
        <button class="w-10/12 bg-purple-600 hover:bg-purple-600 text-white font-medium py-2 px-4 rounded focus:outline-none focus:shadow-outline" type="button"
          @click="startTokenSale"
        >
          Start Token Sale
        </button>
      </div>
      <div v-else class="m-auto d-flex lg:w-2/6 bg-white p-4 rounded .shadow-2xl">
        <h2 class="block mb-4 px-6 font-bold text-left text-black">
          Tokensale is only available on these networks:
        </h2>
        <h4 class="text-md text-left px-6">-Ethereum (Mainnet)</h4>
        <h4 class="text-md text-left px-6 mt-4">-Binance Smart Chain (Mainnet)</h4>
        <button class="w-full bg-purple-600 hover:bg-purple-600 text-white font-medium py-2 px-4 rounded focus:outline-none focus:shadow-outline mt-6" type="button"
          @click="switchNetwork('0x1')"
        >
          Switch
        </button>
      </div>
    </section>
  </div>
</template>

<script>
import Web3 from "web3"
import TokenSaleJson from "../../contracts/TokenSale.json"
import EthxJson from "../../contracts/Ethx.json"
export default {
  name: 'Tokensale',
  components:{
  },
  computed: {
    web3 () {
      return this.$store.state.web3
    },
    chainId(){
      if(this.$store.state.web3.isInjected==false) {
        return 1
      }else {
        return this.$store.state.myChainId
      }
    },
    recvAmount:{
      get(){
        return this.sendAmount*this.price;
      },
      set(newVal){
        this.sendAmount = newVal/this.price;
      }
    }
  },
  data(){
    return {
      web3Obj : new Web3(Web3.givenProvider || 'ws://localhost:8545'),
      contractObj : {},
      ethxContractObj : {},
      price:0,
      minimum:0,
      maximum:0,
      cap:0,
      ethereum:window.ethereum,
      contractAddr:"",
      ethxContractAddr:"",
      abi:TokenSaleJson.abi,
      ethxAbi:EthxJson.abi,
      alertShow:false,
      alertMsg:"",
      networkId:"1",
      account:"0x78Ce09cF3AFF8Ca7cF8B111725f547e18aF74875",
      is_paused:false,
      curCoin:{ sym:"ETH", icon:"../assets/img/icons/icon.png" },
      dropdownShow:false,
    }
  },

  created() {
    console.log("created")
    if(window.ethereum){
      this.web3Obj.eth.getAccounts().then((result)=>{
        this.account = result[0];
      })
      window.ethereum.on('networkChanged', (networkId)=>{
        this.networkChanged(networkId);
      });
      window.ethereum.on('accountsChanged', async (accounts) =>{
        this.account = accounts[0];
        this.getBalance();
      });
    }

    this.web3Obj.eth.net.getId().then((result)=>{
      this.networkChanged(result)
    });
  },
  methods:{
    async switchNetwork(netid){
      this.dropdownShow = false;
      await window.ethereum.request({
        method: 'wallet_switchEthereumChain',
        params: [{ chainId: netid }],
      });
    },
    networkChanged(networkId){
      this.dropdownShow = false;
      this.networkId = networkId;
      if(networkId==1){// ETH Main
        this.contractAddr="0xe5495e47b7cf5c9303ec6ac6b10d27241e0a67b2";
        this.curCoin = { sym:"ETH", icon:"../assets/img/icons/icon.png" };
      } else if(networkId==3){// Ropsten
        this.contractAddr="0x5444b0d07Ef839cCEa4a81FBf999149a06f010fE";
        this.curCoin = { sym:"ETH", icon:"../assets/img/icons/icon.png" };
      } else if(networkId==56){// BSC Mainnet
        this.contractAddr="0x0AaFB655636890a1683c1b5cCFAbD12Efd839D09";
        this.curCoin = { sym:"BNB", icon:"../assets/img/icons/bnb.png" };
      } else if(networkId==97){// BSC Test
        this.contractAddr="0xdBD2014EAed065d9bc0efBC18327DEb32A770DD0";
        this.curCoin = { sym:"BNB", icon:"../assets/img/icons/bnb.png" };
      } else { // 
        this.contractAddr="0x5444b0d07Ef839cCEa4a81FBf999149a06f010fE";
        this.curCoin = { sym:"ETH", icon:"../assets/img/icons/icon.png" };
      }
      
      this.getBalance();
      this.contractObj = new this.web3Obj.eth.Contract(this.abi,this.contractAddr);
      console.log(this.contractObj.methods);
    },

    async getBalance() {
      await this.web3Obj.eth.getBalance(this.account).then((result)=>{
        this.balance = Math.round(100*Web3.utils.fromWei(result, 'ether'))/100;
        console.log(this.balance);
      });
    },
    setPrice: function() {

    },
    setAirdropAmount: function() {

    },
    startTokenSale: function() {

    },
    showAlert:function(msg) {
      this.alertShow=true;
      this.alertMsg=msg;
      setTimeout(()=>{
        this.alertMsg = "";
        this.alertShow = false;
      },1000)
    },
    connectMetaMask: async function() {
      await window.ethereum.request({ method: 'eth_requestAccounts' }).then((result)=>{
        console.log("result="+result)
        document.location.reload();
      });
    }
  }
}
</script>

