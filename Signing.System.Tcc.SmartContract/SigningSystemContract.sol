pragma solidity 0.5.12;

pragma experimental ABIEncoderV2;

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
        require(!RegisteredImages[imageHash], "The Image already is registred!");
        _;
    }
    
    struct Author {
        string Document;
    }
    
    struct Image {
        string Hash;
        uint CreatedAt;
        string AthorDocument;
    }
    
    uint totalAuthors;
    
    mapping (uint => Author[]) Authors;
    
    mapping (string => Image[]) AuthorImages;
    
    mapping (string => bool) RegisteredImages;
    
    function registerDocument(string memory authorDocument, string memory imageHash) public onlyOwner imageExist(imageHash) {
        require(bytes(authorDocument).length > 0, "AuthorDocument CAN'T BE EMPTY!");
        require(bytes(imageHash).length > 0, "ImageHash CAN'T BE EMPTY!");
        
        Author memory newAuthor = Author(authorDocument);
        
        Image memory newImage = Image(imageHash, now, newAuthor.Document);
        
        //VERIFICAR SE NAO VAI CRIAR VARIOS AUTORES COM O MESMO DOC
        Authors[totalAuthors++].push(newAuthor);
        
        AuthorImages[authorDocument].push(newImage);
        
        RegisteredImages[imageHash] = true;
    }
    
    function getImageByAuthor(string memory authorDocument) public view returns (Image [] memory) {
        return AuthorImages[authorDocument];
    }
    
    function verifyImage(string memory imageHash) public view returns (string memory) {
        
    }
}