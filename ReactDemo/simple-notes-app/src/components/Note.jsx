function Note({ text, date }) {
  text = "Changed";
  return (
    <div>
      <p>{text}</p>
      <small>{date}</small>
    </div>
  );
}

export default Note;
