pragma solidity ^0.8.3;

// "SPDX-License-Identifier: UNLICENSED"

import "../node_modules/@openzeppelin/contracts/token/ERC721/extensions/ERC721Enumerable.sol";
import "../node_modules/@openzeppelin/contracts/token/ERC721/extensions/ERC721URIStorage.sol";
import "../node_modules/@openzeppelin/contracts/utils/Context.sol";


contract AceNFT is Context, ERC721Enumerable, ERC721URIStorage {

    constructor() ERC721("Cody's NFT's", "ACE") { }
    
    function _baseURI() internal pure override returns (string memory) {
        return "https://gateway.pinata.cloud/ipfs/";
    }

    function giveToken(address _to, uint _id, string memory _url) public returns(string memory) {
        _mint(_to, _id);
        _setTokenURI(_id, _url);
        return _url;
    }
    
    function _beforeTokenTransfer(address from, address to, uint256 tokenId) internal virtual override(ERC721, ERC721Enumerable) {
        super._beforeTokenTransfer(from, to, tokenId);
    }

    function _burn(uint tokenId) internal override(ERC721, ERC721URIStorage) {
        super._burn(tokenId);
    }

    function tokenURI(uint tokenId) public view override(ERC721, ERC721URIStorage) returns(string memory tokenUri){
        return super.tokenURI(tokenId);
    }

    // fallback() external payable { }

    /**
     * @dev See {IERC165-supportsInterface}.
     */
    function supportsInterface(bytes4 interfaceId) public view virtual override(ERC721, ERC721Enumerable) returns (bool) {
        return super.supportsInterface(interfaceId);
    }
}