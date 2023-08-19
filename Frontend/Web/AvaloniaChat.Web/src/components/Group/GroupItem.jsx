import logo from "../../logo.svg"
export const GroupItem = ({ onGroupClick, group }) => {
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

        <div class="d-flex justify-content-between">
            <div class="d-flex flex-row"  onClick={() => {
                        onGroupClick(group.groupId);
                    }} style={{cursor : "pointer"}}>

                    <img
                        src={
                            group.groupLogo != null
                                ? `data:image/jpeg;base64,${group.groupLogo}`
                                : logo
                        }
                        alt="avatar"
                        class="rounded-circle d-flex align-self-center me-3 shadow-1-strong"
                        width="54"
                        height="54"
                    ></img>
                    <p class="justify-content-center mb-0 align-baseline">
                        {group.groupTitle}
                    </p>
                
            </div>
        </div>
    );
}
