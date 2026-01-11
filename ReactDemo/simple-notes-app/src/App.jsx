import { useEffect, useState } from "react";
import Header from "./components/Header";
import NoteInput from "./components/NoteInput";
import NoteList from "./components/NoteList";

function App() {
  const [notes, setNotes] = useState(() => {
    const storedNotes = localStorage.getItem("notes");
    return storedNotes ? JSON.parse(storedNotes) : [];
  });


  function addNote(newNote) {
    setNotes((prevNotes) => [...prevNotes, newNote]);
  }

   useEffect(() => {
    const storedNotes = localStorage.getItem("notes");

    if (storedNotes) {
      setNotes(JSON.parse(storedNotes));
    }
  }, []);

  useEffect(() => {
    console.log("Setting notes")
    localStorage.setItem("notes", JSON.stringify(notes));
  }, [notes]);


  return (
    <div>
      <Header />
      <NoteInput onAddNote={addNote} />

      {notes.length === 0 ? (
        <p>No notes yet. Add one!</p>
      ) : (
        <NoteList notes={notes} />
      )}
    </div>
  );
}

export default App;