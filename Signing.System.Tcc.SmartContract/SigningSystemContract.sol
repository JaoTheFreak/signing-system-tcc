pragma solidity 0.5.11;

//pragma experimental ABIEncoderV2;

contract SigningSystemContract {
    
    address private contractOwner;
    
    constructor() public {
        contractOwner = msg.sender;
    }

    modifier onlyOwner {
        require(msg.sender == contractOwner, "Only Smart Contract Owner can do this!");
        _;
    }
    
    modifier imageExist(string memory imageHash) {
        require(bytes(RegisteredImages[imageHash].Hash).length == 0, "The Image is already registred!");
        _;
    }
    
    struct Image {
        string Hash;
        uint CreatedAt;
        string AuthorDocument;
    }
    
    mapping (string => Image[]) public AuthorImages;
    
    mapping (string => Image) public RegisteredImages;
    
    function registerDocument(string memory authorDocument, string memory imageHash) public onlyOwner imageExist(imageHash) {
        require(bytes(authorDocument).length > 0, "AuthorDocument can't be empty!");
        require(bytes(imageHash).length > 0, "ImageHash can't be empty!");
        
        Image memory newImage = Image(imageHash, now, authorDocument);
        
        AuthorImages[authorDocument].push(newImage);
        
        RegisteredImages[imageHash] = newImage;
    }
    
    /*function getImageByAuthor(string memory authorDocument) public view returns (Image [] memory images) {
        images = AuthorImages[authorDocument]; 
        
        return images;
    }
    
    function verifyImage(string memory imageHash) public view returns (Image memory foundedImage) {
        foundedImage = RegisteredImages[imageHash];
        
        return foundedImage;
    }*/
}