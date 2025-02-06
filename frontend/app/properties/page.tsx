'use client'

import { useState } from 'react'
import { useQuery } from '@tanstack/react-query'
import { PropertyFilters } from '@/components/properties/PropertyFilters'
import { PropertyCard } from '@/components/properties/PropertyCard'
import { Button } from '@/components/ui/button'

export default function PropertiesPage() {
  const [filters, setFilters] = useState({})

  const { data: properties = [], isLoading } = useQuery({
    queryKey: ['properties', filters],
    queryFn: () => fetch(`${process.env.NEXT_PUBLIC_API_URL}/properties`).then(res => res.json())
  })

  return (
    <div className="container mx-auto py-8">
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold">İlanlar</h1>
        <Button>İlan Ver</Button>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-4 gap-6">
        <div className="md:col-span-1">
          <PropertyFilters onFilter={setFilters} />
        </div>

        <div className="md:col-span-3">
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
            {properties.map((property) => (
              <PropertyCard key={property.id} property={property} />
            ))}
          </div>
          
          {properties.length === 0 && (
            <div className="text-center py-10">
              <p className="text-gray-500">İlan bulunamadı</p>
            </div>
          )}
        </div>
      </div>
    </div>
  )
} 