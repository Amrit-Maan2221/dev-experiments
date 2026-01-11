import Note from "./Note";

function NoteList({ notes }) {
  return (
    <div>
      {notes.map((note, index) => (
        <Note key={index} text={note} />
      ))}
    </div>
  );
}

export default NoteList;