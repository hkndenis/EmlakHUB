import { Property } from '@/types/property'
import { formatMoney } from '@/lib/utils'

interface PropertyDetailsProps {
  property: Property
}

export function PropertyDetails({ property }: PropertyDetailsProps) {
  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-bold">{property.title}</h1>
        <div className="text-2xl font-bold text-primary">
          {formatMoney(property.price)}
        </div>
      </div>

      <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
        <div className="p-4 bg-gray-50 rounded-lg">
          <div className="text-sm text-gray-500">İlan Tipi</div>
          <div className="font-medium">
            {property.type === 'sale' ? 'Satılık' : 'Kiralık'}
          </div>
        </div>
        <div className="p-4 bg-gray-50 rounded-lg">
          <div className="text-sm text-gray-500">Oda Sayısı</div>
          <div className="font-medium">{property.bedrooms}</div>
        </div>
        <div className="p-4 bg-gray-50 rounded-lg">
          <div className="text-sm text-gray-500">Banyo Sayısı</div>
          <div className="font-medium">{property.bathrooms}</div>
        </div>
        <div className="p-4 bg-gray-50 rounded-lg">
          <div className="text-sm text-gray-500">Brüt m²</div>
          <div className="font-medium">{property.squareMeters}</div>
        </div>
      </div>

      <div>
        <h2 className="text-lg font-semibold mb-2">Açıklama</h2>
        <p className="text-gray-600 whitespace-pre-line">{property.description}</p>
      </div>

      <div>
        <h2 className="text-lg font-semibold mb-2">Konum</h2>
        <p className="text-gray-600">
          {property.location.city}, {property.location.district}
        </p>
      </div>
    </div>
  )
} 