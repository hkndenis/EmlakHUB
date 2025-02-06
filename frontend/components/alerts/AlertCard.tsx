'use client'

import { formatMoney } from '@/lib/utils'
import { PropertyType } from '@/types/enums'
import { Button } from '@/components/ui/button'
import { Card, CardContent, CardFooter, CardHeader, CardTitle } from '@/components/ui/card'

interface AlertCardProps {
  alert: {
    id: string
    city: string
    district?: string
    minPrice?: number
    maxPrice?: number
    type: PropertyType
    createdAt: string
  }
}

export function AlertCard({ alert }: AlertCardProps) {
  const location = [alert.city, alert.district].filter(Boolean).join(', ')
  const priceRange = [
    alert.minPrice && `min: ${formatMoney(alert.minPrice)}`,
    alert.maxPrice && `max: ${formatMoney(alert.maxPrice)}`
  ].filter(Boolean).join(' - ')

  return (
    <Card>
      <CardHeader>
        <CardTitle className="flex items-center justify-between">
          <span>{location}</span>
          <span className="text-sm font-normal text-muted-foreground">
            {alert.type === PropertyType.ForSale ? 'Satılık' : 'Kiralık'}
          </span>
        </CardTitle>
      </CardHeader>
      <CardContent>
        <div className="text-sm text-muted-foreground">
          {priceRange || 'Fiyat sınırı yok'}
        </div>
      </CardContent>
      <CardFooter className="flex justify-between">
        <Button variant="ghost" size="sm">
          Düzenle
        </Button>
        <Button variant="destructive" size="sm">
          Sil
        </Button>
      </CardFooter>
    </Card>
  )
} 