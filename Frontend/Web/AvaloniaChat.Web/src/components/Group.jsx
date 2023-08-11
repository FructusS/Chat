export default function Group({ group }) {
  return (
    //   <div
    //     // className={`chat-bubble ${message.uid === user.uid ? "right" : ""}`}>
    //     // <img
    //     //   className="chat-bubble__left"
    //     //   src={message.avatar}
    //     //   alt="user avatar"
    //     // />
    //     // <div className="chat-bubble__right">
    //   <p className="user-name">{message.name}</p>
    //   <p className="user-message">{message.text}</p>
    //     // </div>
    //   </div>
    <div>
      <img src={`data:image/jpeg;base64,${group.groupLogo}`} />
      <p className="user-name">{group.groupName}</p>
    </div>
    
  );
}
