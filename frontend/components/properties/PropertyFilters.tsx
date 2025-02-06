'use client'

import { useState } from 'react'
import { useQuery } from '@tanstack/react-query'
import { Combobox } from '@/components/ui/combobox'
import { Label } from '@/components/ui/label'
import { Input } from '@/components/ui/input'
import { Button } from '@/components/ui/button'
import { PropertyType } from '@/types/enums'

interface PropertyFiltersProps {
  onFilter: (filters: any) => void
}

export function PropertyFilters({ onFilter }: PropertyFiltersProps) {
  const [selectedCity, setSelectedCity] = useState('')
  const [selectedDistrict, setSelectedDistrict] = useState('')
  const [type, setType] = useState<PropertyType>(PropertyType.ForSale)
  const [minPrice, setMinPrice] = useState('')
  const [maxPrice, setMaxPrice] = useState('')
  const [minBedrooms, setMinBedrooms] = useState('')
  const [minSquareMeters, setMinSquareMeters] = useState('')

  const { data: cities = [], error: citiesError, isLoading: isCitiesLoading } = useQuery({
    queryKey: ['cities'],
    queryFn: async () => {
      const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/addresses/cities`);
      if (!response.ok) {
        throw new Error('Şehirler yüklenirken hata oluştu');
      }
      const data = await response.json();
      return data.map((city: string) => ({
        label: city,
        value: city,
      }));
    },
  })

  if (citiesError) {
    console.error('Cities error:', citiesError);
  }

  const { data: districts = [], isLoading: isDistrictsLoading } = useQuery({
    queryKey: ['districts', selectedCity],
    queryFn: async () => {
      if (!selectedCity) return []
      const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/addresses/districts?city=${selectedCity}`)
      if (!response.ok) {
        throw new Error('İlçeler yüklenirken hata oluştu')
      }
      const data = await response.json()
      return data.map((district: string) => ({
        label: district,
        value: district,
      }))
    },
    enabled: !!selectedCity,
  })

  const handleFilter = () => {
    onFilter({
      city: selectedCity,
      district: selectedDistrict,
      type,
      minPrice: minPrice ? Number(minPrice) : undefined,
      maxPrice: maxPrice ? Number(maxPrice) : undefined,
      minBedrooms: minBedrooms ? Number(minBedrooms) : undefined,
      minSquareMeters: minSquareMeters ? Number(minSquareMeters) : undefined,
    })
  }

  return (
    <div className="space-y-4 p-4 border rounded-lg bg-white">
      <div className="grid gap-4 md:grid-cols-2">
        <div>
          <Label>Şehir</Label>
          <Combobox
            options={cities}
            value={selectedCity}
            onChange={(value) => {
              setSelectedCity(value)
              setSelectedDistrict('')
            }}
            placeholder={isCitiesLoading ? "Yükleniyor..." : "Şehir seçiniz"}
            disabled={isCitiesLoading}
          />
        </div>

        <div>
          <Label>İlçe</Label>
          <Combobox
            options={districts}
            value={selectedDistrict}
            onChange={setSelectedDistrict}
            placeholder={isDistrictsLoading ? "Yükleniyor..." : "İlçe seçiniz"}
            disabled={!selectedCity || isDistrictsLoading}
          />
        </div>
      </div>

      <div className="grid gap-4 md:grid-cols-2">
        <div>
          <Label>Min. Fiyat</Label>
          <Input 
            type="number" 
            value={minPrice} 
            onChange={(e) => setMinPrice(e.target.value)}
            placeholder="0"
          />
        </div>

        <div>
          <Label>Max. Fiyat</Label>
          <Input 
            type="number" 
            value={maxPrice} 
            onChange={(e) => setMaxPrice(e.target.value)}
            placeholder="0"
          />
        </div>
      </div>

      <div className="grid gap-4 md:grid-cols-2">
        <div>
          <Label>İlan Tipi</Label>
          <Combobox
            options={[
              { label: 'Satılık', value: PropertyType.ForSale },
              { label: 'Kiralık', value: PropertyType.ForRent },
            ]}
            value={type}
            onChange={(value) => setType(value as PropertyType)}
            placeholder="İlan tipi seçiniz"
          />
        </div>

        <div>
          <Label>Min. Oda Sayısı</Label>
          <Input 
            type="number" 
            value={minBedrooms} 
            onChange={(e) => setMinBedrooms(e.target.value)}
            placeholder="0"
          />
        </div>
      </div>

      <div>
        <Label>Min. m²</Label>
        <Input
          type="number"
          value={minSquareMeters}
          onChange={(e) => setMinSquareMeters(e.target.value)}
          placeholder="0"
        />
      </div>

      <Button onClick={handleFilter} className="w-full">Filtrele</Button>
    </div>
  )
} 