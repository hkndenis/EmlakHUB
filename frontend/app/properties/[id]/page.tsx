'use client'

import { useQuery } from '@tanstack/react-query'
import { PropertyGallery } from '@/components/properties/PropertyGallery'
import { PropertyDetails } from '@/components/properties/PropertyDetails'
import { PropertyFeatures } from '@/components/properties/PropertyFeatures'
import { PropertyMap } from '@/components/properties/PropertyMap'

export default function PropertyDetailPage({ params }: { params: { id: string } }) {
  const { data: property, isLoading } = useQuery({
    queryKey: ['property', params.id],
    queryFn: () => fetch(`${process.env.NEXT_PUBLIC_API_URL}/properties/${params.id}`).then(res => res.json())
  })

  if (isLoading) return <div>Yükleniyor...</div>
  if (!property) return <div>İlan bulunamadı</div>

  return (
    <div className="container mx-auto py-8">
      <PropertyGallery images={property.images} />
      <div className="grid grid-cols-1 md:grid-cols-3 gap-8 mt-8">
        <div className="md:col-span-2">
          <PropertyDetails property={property} />
          <PropertyFeatures features={property.features} />
        </div>
        <div>
          <PropertyMap location={property.location} />
        </div>
      </div>
    </div>
  )
} 