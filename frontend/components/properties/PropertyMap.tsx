'use client'

import { useEffect, useRef } from 'react'
import L from 'leaflet'
import 'leaflet/dist/leaflet.css'

interface PropertyMapProps {
  location: {
    latitude: number
    longitude: number
  }
}

export function PropertyMap({ location }: PropertyMapProps) {
  const mapRef = useRef<HTMLDivElement>(null)
  const mapInstanceRef = useRef<L.Map | null>(null)

  useEffect(() => {
    if (!mapRef.current) return

    // Leaflet map'i başlat
    if (!mapInstanceRef.current) {
      mapInstanceRef.current = L.map(mapRef.current).setView(
        [location.latitude, location.longitude],
        15
      )

      // OpenStreetMap tile layer'ı ekle
      L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '© OpenStreetMap contributors'
      }).addTo(mapInstanceRef.current)

      // Marker ekle
      L.marker([location.latitude, location.longitude]).addTo(mapInstanceRef.current)
    } else {
      // Map zaten varsa sadece view'ı güncelle
      mapInstanceRef.current.setView([location.latitude, location.longitude], 15)
    }

    // Cleanup
    return () => {
      if (mapInstanceRef.current) {
        mapInstanceRef.current.remove()
        mapInstanceRef.current = null
      }
    }
  }, [location])

  return <div ref={mapRef} className="h-[400px] w-full rounded-lg" />
} 