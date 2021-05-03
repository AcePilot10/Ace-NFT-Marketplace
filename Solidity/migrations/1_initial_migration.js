// const Migrations = artifacts.require("Migrations");
const nft = artifacts.require("AceNFT");

module.exports = function (deployer) {
  // deployer.deploy(Migrations);
  deployer.deploy(nft);
  // deployer.deploy(nft);
};