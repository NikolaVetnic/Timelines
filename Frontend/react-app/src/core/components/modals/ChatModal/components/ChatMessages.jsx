import { Message } from "./Message";

export const ChatMessages = ({ activeMessages, messagesEndRef }) => (
  <div className="chat-messages">
    {activeMessages.map((message) => (
      <Message key={message.id || message.text} message={message} />
    ))}
    <div ref={messagesEndRef} />
  </div>
);
