import { useState } from "react";

function NoteInput({ onAddNote }) {
  const [text, setText] = useState("");

  function handleChange(e) {
    setText(e.target.value);
  }

  function handleAdd() {
    if (text.trim() === "") return;

    onAddNote(text);
    setText(""); // clear input
  }

  return (
    <div>
      <input
        value={text}
        onChange={handleChange}
        placeholder="Write a note..."
      />
      <button onClick={handleAdd}>Add</button>
    </div>
  );
}

export default NoteInput;