import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    port: 5173, // Frontendi port
    proxy: {
      // Kõik päringud, mis algavad /api, suunatakse edasi .NET backendile
      '/api': {
        target: 'http://localhost:5279', // C# backendi HTTP aadress
        changeOrigin: true,
        secure: false, // Lubab ka self-signed (omatehtud) SSL sertifikaate, mida .NET arenduses kasutab
      }
    }
  }
})
