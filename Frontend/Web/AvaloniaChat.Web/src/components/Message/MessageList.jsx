export const MessageList = ({ messages }) => {
    return (
            <ul className="list-unstyled" data-mdb-perfect-scrollbar="true"  data-mdb-suppress-scroll-x="true" style={{height: '400px' , position: 'relative'}}>
                {messages?.map(
                    (message) => (
                        <li >
                            <div className="card">
                                <div className="card-header d-flex justify-content-between">
                                    <p className="fw-bold mb-0">{message.username}</p>
                                    <p className="text-muted small mb-0">
                                        <i className="far fa-clock"></i> {message.sendDate}
                                    </p>
                                </div>
                                <div className="card-body">
                                    <p className="mb-0">
                                        {message.messageText}
                                    </p>
                                </div>
                            </div>
                        </li>
                    )

                    // <li class="p-2 border-bottom">

                    // </li>
                )}
            </ul>
    );
}
