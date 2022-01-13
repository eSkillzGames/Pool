
async function main() {
  // This is just a convenience check
  if (network.name === "hardhat") {
    console.warn(
      "You are trying to deploy a contract to the Hardhat Network, which" +
        "gets automatically created and destroyed every time. Use the Hardhat" +
        " option '--network localhost'"
    );
  }

  // ethers is avaialble in the global scope
  const [deployer] = await ethers.getSigners();
  console.log(
    "Deploying the contracts with the account:",
    await deployer.getAddress()
  );

  console.log("Account balance:", (await deployer.getBalance()).toString());

  const Token = await ethers.getContractFactory("EmissionPool");
  //const token = await Token.deploy("0xd99d1c33f9fc3444f8101754abc46c52416550d1");// BSC_TEST 0xd99d1c33f9fc3444f8101754abc46c52416550d1
  const token = await Token.deploy("0xFbD1272C17B300B2040133E079bFa4d9C7B7f971");// ROPSTEN 0x7a250d5630b4cf539739df2c5dacb4c659f2488d
  
  await token.deployed();

  console.log("Token address:", token.address);
}


main()
  .then(() => process.exit(0))
  .catch((error) => {
    console.error(error);
    process.exit(1);
  });
