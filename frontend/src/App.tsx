import { BrowserRouter, Route, Routes } from "react-router-dom"
import './App.css'
import PlanetsList from "./views/PlanetsList.tsx";

function App() {

  return (
    <BrowserRouter>
        <Routes>
            <Route path="/planets" element={<PlanetsList />}/>
        </Routes>
    </BrowserRouter>
  )
}

export default App
