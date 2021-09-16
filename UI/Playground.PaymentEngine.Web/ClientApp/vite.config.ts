import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
  server: {
    port: 7003,
    https: true,
    proxy: {
      "/WeatherForecast": "https://localhost:7001/WeatherForecast"
    }
  },
  plugins: [vue()]
})
