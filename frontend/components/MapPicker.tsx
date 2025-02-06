import { useState } from 'react'
import { GoogleMap, Marker } from '@react-google-maps/api'

interface Location {
  lat: number
  lng: number
}

interface SearchParams {
  latitude: number
  longitude: number
  radiusInKm: number
}

const defaultCenter = {
  lat: 38.4192, // İzmir merkez
  lng: 27.1287
}

export const MapPicker = () => {
  const [selectedLocation, setSelectedLocation] = useState<Location | null>(null)

  const handleMapClick = (event: google.maps.MapMouseEvent) => {
    if (!event.latLng) return
    
    const lat = event.latLng.lat()
    const lng = event.latLng.lng()
    setSelectedLocation({ lat, lng })
    
    // Yakındaki ilanları getir
    searchProperties({ 
      latitude: lat, 
      longitude: lng, 
      radiusInKm: 2 
    })
  }

  return (
    <div className="h-[400px] w-full">
      <GoogleMap
        mapContainerClassName="w-full h-full"
        center={defaultCenter}
        zoom={13}
        onClick={handleMapClick}
      >
        {selectedLocation && (
          <Marker position={selectedLocation} />
        )}
      </GoogleMap>
    </div>
  )
} 