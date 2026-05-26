import {BrowserRouter, Navigate, Route, Routes} from "react-router-dom"
import './App.css'
import PlanetsList from "./views/PlanetsList.tsx";
import PlanetDetails from "./views/PlanetDetails.tsx";
import PlanetsEdit from "./views/PlanetsEdit.tsx";

function App() {

  return (
    <BrowserRouter>
        <Routes>
            <Route path="/" element={<Navigate to="/planets" replace />} />
            <Route path="/planets" element={<PlanetsList />}/>
            <Route path="/planets/:planetsId" element={<PlanetDetails />} />
            <Route path="/planets/:planetsId/edit" element={<PlanetsEdit />} />
        </Routes>
    </BrowserRouter>
  )
}

export default App
