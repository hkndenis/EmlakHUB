import { useState } from 'react'
import { useQuery } from '@tanstack/react-query'

interface Coordinates {
  latitude: number
  longitude: number
  formattedAddress: string
}

export const AddressSearch = () => {
  const [address, setAddress] = useState('')
  const [city, setCity] = useState('')
  const [district, setDistrict] = useState('')

  const { data, isLoading } = useQuery<Coordinates>({
    queryKey: ['coordinates', address, city, district],
    queryFn: async () => {
      const response = await fetch(
        `/api/Addresses/coordinates?address=${address}&city=${city}&district=${district}`
      )
      return response.json()
    },
    enabled: !!address && !!city // Sadece adres ve şehir varsa çalışsın
  })

  return (
    <div className="space-y-4">
      <input
        type="text"
        placeholder="Adres"
        value={address}
        onChange={(e) => setAddress(e.target.value)}
        className="input input-bordered w-full"
      />
      <div className="grid grid-cols-2 gap-4">
        <input
          type="text"
          placeholder="Şehir"
          value={city}
          onChange={(e) => setCity(e.target.value)}
          className="input input-bordered"
        />
        <input
          type="text"
          placeholder="İlçe"
          value={district}
          onChange={(e) => setDistrict(e.target.value)}
          className="input input-bordered"
        />
      </div>
      {isLoading && <div>Koordinatlar bulunuyor...</div>}
      {data && (
        <div>
          <p>Enlem: {data.latitude}</p>
          <p>Boylam: {data.longitude}</p>
          <p>Adres: {data.formattedAddress}</p>
        </div>
      )}
    </div>
  )
} 