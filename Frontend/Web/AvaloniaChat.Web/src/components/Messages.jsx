export default function Messages({ messages }) {
    return (
            <ul class="list-unstyled" data-mdb-perfect-scrollbar="true"  data-mdb-suppress-scroll-x="true" style={{height: '400px' , position: 'relative'}}>
                {messages?.map(
                    (message) => (
                        <li >
                            <div class="card">
                                <div class="card-header d-flex justify-content-between">
                                    <p class="fw-bold mb-0">{message.username}</p>
                                    <p class="text-muted small mb-0">
                                        <i class="far fa-clock"></i> {message.sendDate}
                                    </p>
                                </div>
                                <div class="card-body">
                                    <p class="mb-0">
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
