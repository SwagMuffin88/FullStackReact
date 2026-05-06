import {useCallback, useEffect, useState} from 'react';
import type {Planet} from "../types/types.tsx";
import { useNavigate } from "react-router-dom";

function PlanetsList(){
    const [planets, setPlanets] = useState<Planet[]>([])
    const navigate = useNavigate()
    
    const fetchPlanets = useCallback(async () => {
        try {
            const response = await fetch("/api/Planets")
            
            if (response.ok) {
                const data = await response.json()
                setPlanets(data)
            }
        } catch (error) {
            console.error("Fetch error: ", error)
        }
    }, [])

    useEffect(() => {
        fetchPlanets()
    }, [fetchPlanets])
    
    return (
        <div>
            <h2>Kosmose planeedid</h2>
            {planets.length === 0 ? (
                <p>Laadin planeete või andmebaas on tühi...</p>
            ) : (
                <ul>
                    {planets.map((planet) => (
                        <li key={planet.planetId} style={{ margin: '10px 0' }}>
                            <strong>{planet.name}</strong> – Mass: {planet.mass} Maad
                            <button onClick={() => navigate(`/planets/${planet.planetId}`)} 
                                    style={{ marginLeft: '10px' }}
                            >
                                Vaata lähemalt
                            </button>
                        </li>
                    ))}
                </ul>
            )}
        </div>
    )
}

export default PlanetsList;