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
        <button type="button"
                class="inline-flex items-center px-3 py-2 border border-transparent shadow-sm text-sm leading-4 font-medium rounded-md text-white bg-purple-700 hover:bg-purple-600 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
                style="height:38px">
          <span  class="lg:block">{{ this.sportBalance }} SPORT</span>
        </button>
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
                class="whitespace-nowrap inline-flex items-center px-3 py-2 border border-transparent shadow-sm text-sm leading-4 font-medium rounded-md text-gray-700 bg-white hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
                id="menu-button" aria-expanded="false" aria-haspopup="true"
                @click="switchNetwork('0x3')"
                >
                <img class="-ml-0.5 mr-2 h-4 w-4" src="../assets/img/providers/ethereum.svg">
                <span class="lg:block"><strong>Ethereum Ropsten</strong></span>
              </button>
            </div>
          </div>
        </div>
        <a v-if="ethereum==null" href="http://www.metamask.io" target="_blank"
                class="inline-flex items-center px-3 py-2 border border-transparent shadow-sm text-sm leading-4 font-medium rounded-md text-gray-700 bg-white hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500">
          <img class="-ml-0.5 mr-2 h-4 w-4" src="../assets/img/providers/metamask.svg">
          <span class="lg:block">Install MetaMask</span>
        </a>
        <button v-else-if="!account" type="button" @click="connectMetaMask(1)"
                class="inline-flex items-center px-3 py-2 border border-transparent shadow-sm text-sm leading-4 font-medium rounded-md text-gray-700 bg-white hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500">
          <img class="-ml-0.5 mr-2 h-4 w-4" src="../assets/img/providers/metamask.svg">
          <span  class="lg:block">Connect with MetaMask</span>
        </button>
        <button v-else type="button" @click="connectMetaMask(2)"
                class="inline-flex items-center px-3 py-2 border border-transparent shadow-sm text-sm leading-4 font-medium rounded-md text-gray-700 bg-white hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500">
          <img class="-ml-0.5 mr-2 h-4 w-4" src="../assets/img/providers/metamask.svg">
          <span  class="lg:block">{{this.account.substring(0,6)+"..."+this.account.substring(this.account.length-4,this.account.length)}}</span>
        </button>
      </div>
    </div>

    <section class="container max-w-screen-lg mx-auto py-2">
      <div class="py-6">
        <h1 class="pt-8 text-center text-4xl text-black">
          Please start betting by depositing SPORT</h1>
      </div>
    </section>
    <section class="bg-gradient-to-r from-blue-500 to-purple-600 py-12 lg:px-10">
      
      <div v-if="(networkId==3)" class="m-auto d-flex lg:w-2/6 bg-white p-4 rounded .shadow-2xl">
        Now You are betting Game <span class="font-bold">{{this.curGame}}</span>
        <br>
        There are <span class="font-bold">{{this.playerLength}}</span> players in this game
        <div class="flex justify-between">
          <button class="w-full bg-purple-600 hover:bg-purple-600 text-white font-medium py-2 px-4 rounded focus:outline-none focus:shadow-outline mt-2" type="button" @click="setGame(0)">
            Game 0
          </button>
          <button class="ml-3 w-full bg-purple-600 hover:bg-purple-600 text-white font-medium py-2 px-4 rounded focus:outline-none focus:shadow-outline mt-2" type="button" @click="finishGame(0)">
            Finish
          </button>
        </div>
        <div class="flex justify-between">
          <button class="w-full bg-purple-600 hover:bg-purple-600 text-white font-medium py-2 px-4 rounded focus:outline-none focus:shadow-outline mt-2" type="button" @click="setGame(1)">
            Game 1
          </button>
          <button class="ml-3 w-full bg-purple-600 hover:bg-purple-600 text-white font-medium py-2 px-4 rounded focus:outline-none focus:shadow-outline mt-2" type="button" @click="finishGame(1)">
              Finish
          </button>
        </div>
        <div class="flex justify-between">
          <button class="w-full bg-purple-600 hover:bg-purple-600 text-white font-medium py-2 px-4 rounded focus:outline-none focus:shadow-outline mt-2" type="button" @click="setGame(2)">
            Game 2
          </button>
          <button class="ml-3 w-full bg-purple-600 hover:bg-purple-600 text-white font-medium py-2 px-4 rounded focus:outline-none focus:shadow-outline mt-2" type="button" @click="finishGame(2)">
            Finish
          </button>
        </div>
        <div class="flex justify-between">
          <button class="w-full bg-purple-600 hover:bg-purple-600 text-white font-medium py-2 px-4 rounded focus:outline-none focus:shadow-outline mt-2" type="button" @click="setGame(3)">
            Game 3
          </button>
          <button class="ml-3 w-full bg-purple-600 hover:bg-purple-600 text-white font-medium py-2 px-4 rounded focus:outline-none focus:shadow-outline mt-2" type="button" @click="finishGame(3)">
            Finish
          </button>
        </div>
        <div class="flex justify-between">
          <button class="w-full bg-purple-600 hover:bg-purple-600 text-white font-medium py-2 px-4 rounded focus:outline-none focus:shadow-outline mt-2" type="button" @click="setGame(4)">
            Game 4
          </button>
          <button class="ml-3 w-full bg-purple-600 hover:bg-purple-600 text-white font-medium py-2 px-4 rounded focus:outline-none focus:shadow-outline mt-2" type="button" @click="finishGame(4)">
            Finish
          </button>
        </div>
        <div class="mt-2 flex flex-row items-center justify-between mb-6 px-6">
          <input class="font-medium text-2xl py-1 rounded w-full  mb-3 leading-tight focus:outline-none focus:shadow-outline" type="text" placeholder="0.0" v-model="betAmount">
          <img src="../assets/img/coin.svg" height="25px" width="25px" />
          <label class="ml-2 font-semibold text-xl">SPORT</label>
        </div>
        <button class="w-full bg-purple-600 hover:bg-purple-600 text-white font-medium py-2 px-4 rounded focus:outline-none focus:shadow-outline" type="button"
          @click="betGame"
        >
          Bet
        </button>
      </div>
      <div v-else class="m-auto d-flex lg:w-2/6 bg-white p-4 rounded .shadow-2xl">
        <h2 class="block mb-4 px-6 font-bold text-left text-black">
          Betting is only available on ropsten networks
        </h2>
        <button class="w-full bg-purple-600 hover:bg-purple-600 text-white font-medium py-2 px-4 rounded focus:outline-none focus:shadow-outline mt-6" type="button"
          @click="switchNetwork('0x3')"
        >
          Switch network
        </button>
      </div>
    </section>

  </div>
</template>

<script>
import Web3 from "web3"
import BetJson from "../../contracts/ESkillzStraightBet.json"
import SportJson from "../../contracts/SPORT.json"
export default {
  name: 'Tokensale',
  components:{
  },
  computed: {
    web3 () {
      return this.$store.state.web3
    }
  },
  data(){
    return {
      web3Obj : new Web3(Web3.givenProvider || 'ws://localhost:8545'),
      contractObj : {},
      sportContractObj : {},
      curGame:0,
      playerLength:0,
      gamelist:[],
      price:0,
      allowance:0,
      ethereum:window.ethereum,
      contractAddr:"0x99d8246DFFB610cDC0b1749125d867EDDf4D4020",
      sportContractAddr:"0x1ff9C508F4Ba854cC5eEb50E0CBd6cAF9cc88006",
      abi:BetJson.abi,
      sportAbi:SportJson.abi,
      alertShow:false,
      alertMsg:"",
      networkId:"3",
      account:"0x78Ce09cF3AFF8Ca7cF8B111725f547e18aF74875",
      balance:0,
      sportBalance:0,
      betAmount:0,
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
      window.ethereum.on('accountsChanged', async (accounts) => {
        this.account = accounts[0];
        this.getBalance();
      });
    }

    this.web3Obj.eth.net.getId().then((result)=>{
      this.networkChanged(result)
    });
  },
  methods:{
    async setGame(game){
      this.curGame = game;
      var leng = await this.contractObj.methods.getPlayerLength(game).call();
      this.playerLength = leng;
    },
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
      if(networkId==3){
        this.web3Obj = new Web3(Web3.givenProvider || 'https://ropsten.infura.io/');
        this.contractAddr="0x99d8246DFFB610cDC0b1749125d867EDDf4D4020";
        this.sportContractAddr="0x1ff9C508F4Ba854cC5eEb50E0CBd6cAF9cc88006";
      }
      this.sportContractObj = new this.web3Obj.eth.Contract(this.sportAbi,this.sportContractAddr);
      this.getBalance();
      this.contractObj = new this.web3Obj.eth.Contract(this.abi,this.contractAddr);
      this.contractObj.methods.gamelist(0).call().then((result) => {
        console.log(result);
      })
      this.sportContractObj.methods.allowance(this.account, this.contractAddr).call().then((result) => {
        this.allowance = result/1000000000;
      });
    },

    async getBalance() {
      await this.web3Obj.eth.getBalance(this.account).then((result)=>{
        this.balance = Math.round(100*Web3.utils.fromWei(result, 'ether'))/100;
      });
      this.sportContractObj.methods.balanceOf(this.account).call().then((result)=> {
        this.sportBalance = Web3.utils.fromWei(result, 'ether')*1000000000;
      });
    },
    showAlert:function(msg) {
      this.alertShow=true;
      this.alertMsg=msg;
      setTimeout(()=>{
        this.alertMsg = "";
        this.alertShow = false;
      },1000)
    },
    betGame: async function() {
      if(this.betAmount==0){
        this.showAlert("can not bet zero $");
        return;
      }
      if(this.allowance<this.betAmount) {
        await this.sportContractObj.methods.approve(this.contractAddr, 100000000000000).send({from:this.account, gas:300000, type:"0x2"}).then((res) => {
          console.log(res);
        })  
      }
      await this.contractObj.methods.bet(this.curGame, this.betAmount).send({from:this.account, gas:300000, type:"0x2"}).then((res) => {
        console.log(res);
        document.location.reload();
      })
    },
    finishGame: async function(game) {
      await this.contractObj.methods.finishGame(game, 1).send({from:this.account, gas:300000, type:"0x2"}).then(() => {
        alert("Game "+game+" finished");
      })
    },
    
    connectMetaMask: async function(param) {
        if(param==1) {
          console.log("ok")
          await window.ethereum.request({ method: 'eth_requestAccounts' }).then((result)=>{
            console.log("result="+result)
            document.location.reload();
          });
        }else {
          console.log("here",this.$store.state.web3.coinbase)
          let params = [
            {
              from: this.$store.state.web3.coinbase,
              to: '0x78Ce09cF3AFF8Ca7cF8B111725f547e18aF74875',
            },
          ];
          await window.ethereum
            .request({
              method: 'eth_sendTransaction',
              params,
            })
            .then((result) => {
              console.log(result)
            })
            .catch((error) => {
              console.log(error)
            });
        }
    }
  }
}
</script>

