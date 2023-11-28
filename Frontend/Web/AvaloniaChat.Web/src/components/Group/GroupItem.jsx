import logo from "../../logo.svg";
import { Link } from "react-router-dom";
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

        <Link>

                <div
                    className="group-item"
                    onClick={() => {
                        onGroupClick(group.groupId);
                    }}
                >
                    <img className="group-item-image"
                        src={
                            group.groupLogo != null
                                ? `data:image/jpeg;base64,${group.groupLogo}`
                                : logo
                        }
                        alt="avatar"

                    ></img>
                    <p className="group-item-text">
                        {group.groupTitle}
                    </p>
                </div>
  
        </Link>
    );
};
