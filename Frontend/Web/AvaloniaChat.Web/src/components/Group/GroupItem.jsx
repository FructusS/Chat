import logo from "../../logo.svg";
import { Link } from 'react-router-dom';
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
            <div className="d-flex justify-content-between">
                <div className="d-flex flex-row">
                    <img
                        src={
                            group.groupLogo != null
                                ? `data:image/jpeg;base64,${group.groupLogo}`
                                : logo
                        }
                        alt="avatar"
                        className="rounded-circle d-flex align-self-center me-3 shadow-1-strong"
                        width="54"
                        height="54"
                    ></img>
                    <p className="justify-content-center mb-0 align-baseline">
                        {group.groupTitle}
                    </p>
                </div>
            </div>
        </Link>
    );
};
