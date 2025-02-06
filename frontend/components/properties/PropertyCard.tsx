import { Property } from '@/types/property'
import { formatMoney } from '@/lib/utils'
import Link from 'next/link'
import Image from 'next/image'
import { getImageUrl } from '@/lib/utils'

interface PropertyCardProps {
  property: Property
}

export function PropertyCard({ property }: PropertyCardProps) {
  return (
    <Link href={`/properties/${property.id}`}>
      <div className="border rounded-lg overflow-hidden hover:shadow-lg transition-shadow">
        <div className="aspect-video relative">
          <Image 
            src={getImageUrl(property.images[0]?.url)}
            alt={property.title}
            fill
            className="object-cover"
          />
          <div className="absolute top-2 right-2 bg-white px-2 py-1 rounded">
            {property.type === 'sale' ? 'Satılık' : 'Kiralık'}
          </div>
        </div>
        
        <div className="p-4">
          <h3 className="font-semibold text-lg">{property.title}</h3>
          <p className="text-gray-600 text-sm">{property.location.city} - {property.location.district}</p>
          <div className="mt-2 flex items-center justify-between">
            <p className="font-bold text-lg">{formatMoney(property.price)}</p>
            <div className="text-sm text-gray-600">
              {property.bedrooms} oda • {property.bathrooms} banyo • {property.squareMeters}m²
            </div>
          </div>
        </div>
      </div>
    </Link>
  )
} 