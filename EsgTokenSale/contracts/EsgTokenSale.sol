/**
 * Compiled with 0.6.6+commit.6c089d02
*/

// Partial License: MIT

pragma solidity ^0.6.0;

interface IERC20 {
    function totalSupply() external view returns (uint256);
    function balanceOf(address account) external view returns (uint256);
    function transfer(address recipient, uint256 amount) external returns (bool);
    function allowance(address owner, address spender) external view returns (uint256);
    function approve(address spender, uint256 amount) external returns (bool);
    function transferFrom(address sender, address recipient, uint256 amount) external returns (bool);
    event Transfer(address indexed from, address indexed to, uint256 value);
    event Approval(address indexed owner, address indexed spender, uint256 value);
}


// Partial License: MIT

pragma solidity ^0.6.0;

abstract contract Context {
    function _msgSender() internal view virtual returns (address payable) {
        return msg.sender;
    }

    function _msgData() internal view virtual returns (bytes memory) {
        this; // silence state mutability warning without generating bytecode - see https://github.com/ethereum/solidity/issues/2691
        return msg.data;
    }
}


// Partial License: MIT

pragma solidity ^0.6.0;

contract Ownable is Context {
    address private _owner;
    event OwnershipTransferred(address indexed previousOwner, address indexed newOwner);
    constructor () internal {
        address msgSender = _msgSender();
        _owner = msgSender;
        emit OwnershipTransferred(address(0), msgSender);
    }
    function owner() public view returns (address) {
        return _owner;
    }
    modifier onlyOwner() {
        require(_owner == _msgSender(), "Ownable: caller is not the owner");
        _;
    }
    function renounceOwnership() public virtual onlyOwner {
        emit OwnershipTransferred(_owner, address(0));
        _owner = address(0);
    }
    function transferOwnership(address newOwner) public virtual onlyOwner {
        require(newOwner != address(0), "Ownable: new owner is the zero address");
        emit OwnershipTransferred(_owner, newOwner);
        _owner = newOwner;
    }
}


// Partial License: MIT

pragma solidity ^0.6.0;

library SafeMath {
    function add(uint256 a, uint256 b) internal pure returns (uint256) {
        uint256 c = a + b;
        require(c >= a, "SafeMath: addition overflow");

        return c;
    }
    function sub(uint256 a, uint256 b) internal pure returns (uint256) {
        return sub(a, b, "SafeMath: subtraction overflow");
    }
    function sub(uint256 a, uint256 b, string memory errorMessage) internal pure returns (uint256) {
        require(b <= a, errorMessage);
        uint256 c = a - b;

        return c;
    }
    function mul(uint256 a, uint256 b) internal pure returns (uint256) {
        if (a == 0) {
            return 0;
        }

        uint256 c = a * b;
        require(c / a == b, "SafeMath: multiplication overflow");

        return c;
    }
    function div(uint256 a, uint256 b) internal pure returns (uint256) {
        return div(a, b, "SafeMath: division by zero");
    }
    function div(uint256 a, uint256 b, string memory errorMessage) internal pure returns (uint256) {
        require(b > 0, errorMessage);
        uint256 c = a / b;
        // assert(a == b * c + a % b); // There is no case in which this doesn't hold

        return c;
    }
    function mod(uint256 a, uint256 b) internal pure returns (uint256) {
        return mod(a, b, "SafeMath: modulo by zero");
    }
    function mod(uint256 a, uint256 b, string memory errorMessage) internal pure returns (uint256) {
        require(b != 0, errorMessage);
        return a % b;
    }
}


pragma solidity 0.6.6;



contract EsgTokenSale is Ownable {
    using SafeMath for uint256;
    IERC20 public esg;
    uint256 constant BP = 10000;
    bool    public started;
    uint256 public price;
    uint256 public totalOwed;
    uint256 public weiRaised;

    constructor (address addr) public { esg = IERC20(addr); }
    function setPrice(uint256 _price)       public onlyOwner { price = _price; }
    function unlock() public onlyOwner { started =  false; }

    function withdrawETH(address payable _addr, uint256 amount) public onlyOwner {
        _addr.transfer(amount);
    }

    function withdrawETHOwner(uint256 amount) public onlyOwner {
        msg.sender.transfer(amount);
    }

    function withdrawUnsold(address _addr, uint256 amount) public onlyOwner {
        require(amount <= esg.balanceOf(address(this)).sub(totalOwed), "insufficient balance");
        esg.transfer(_addr, amount);
    }

    // start the presale
    function startPresale() public onlyOwner {
        require(!started, "already started!");
        require(price > 0, "set price first!");
        started = true;
    }

    // the amount of esg purchased
    function calculateAmountPurchased(uint256 _value) public view returns (uint256) {
        return _value.mul(BP).mul(price).div(10**9).div(BP);
    }
    
    // purchase tokens
    function buy() public payable {
        require(started, "token sale is not started");
        uint256 amount = calculateAmountPurchased(msg.value);
        require(amount <= esg.balanceOf(address(this)), "sold out");
        require(esg.transfer(msg.sender, amount), "failed to buy");
        totalOwed = totalOwed.add(amount);
        weiRaised = weiRaised.add(msg.value);
    }

    fallback() external payable { buy(); }
    receive() external payable { buy(); }
}
