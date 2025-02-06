export interface Property {
  id: string
  title: string
  description: string
  price: number
  type: 'sale' | 'rent'
  bedrooms: number
  bathrooms: number
  squareMeters: number
  location: {
    city: string
    district: string
    latitude: number
    longitude: number
  }
  images: Array<{
    id: string
    url: string
    isMain: boolean
  }>
  features: Array<{
    id: string
    name: string
    description: string
  }>
}

export interface PropertyFilters {
  type?: 'sale' | 'rent'
  city?: string
  district?: string
  minPrice?: number
  maxPrice?: number
  minBedrooms?: number
  minSquareMeters?: number
  features?: string[]
} 