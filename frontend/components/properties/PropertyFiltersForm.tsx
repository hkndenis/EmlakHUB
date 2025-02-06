'use client'

import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import * as z from 'zod'
import { PropertyFilters } from '@/types/property'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select'
import { Button } from '@/components/ui/button'

const filterSchema = z.object({
  type: z.enum(['sale', 'rent']).optional(),
  city: z.string().optional(),
  district: z.string().optional(),
  minPrice: z.number().min(0).optional(),
  maxPrice: z.number().min(0).optional(),
  minBedrooms: z.number().min(1).optional(),
  minSquareMeters: z.number().min(1).optional()
})

interface PropertyFiltersFormProps {
  filters: PropertyFilters
  onFilterChange: (filters: PropertyFilters) => void
}

export function PropertyFiltersForm({ filters, onFilterChange }: PropertyFiltersFormProps) {
  const form = useForm<PropertyFilters>({
    resolver: zodResolver(filterSchema),
    defaultValues: filters
  })

  return (
    <form onSubmit={form.handleSubmit(onFilterChange)} className="space-y-4">
      <div>
        <Label>İlan Tipi</Label>
        <Select onValueChange={(value) => form.setValue('type', value as 'sale' | 'rent')}>
          <SelectTrigger>
            <SelectValue placeholder="Tümü" />
          </SelectTrigger>
          <SelectContent>
            <SelectItem value="sale">Satılık</SelectItem>
            <SelectItem value="rent">Kiralık</SelectItem>
          </SelectContent>
        </Select>
      </div>

      <div>
        <Label>Şehir</Label>
        <Input {...form.register('city')} />
      </div>

      <div>
        <Label>İlçe</Label>
        <Input {...form.register('district')} />
      </div>

      <div className="grid grid-cols-2 gap-4">
        <div>
          <Label>Min. Fiyat</Label>
          <Input type="number" {...form.register('minPrice', { valueAsNumber: true })} />
        </div>
        <div>
          <Label>Max. Fiyat</Label>
          <Input type="number" {...form.register('maxPrice', { valueAsNumber: true })} />
        </div>
      </div>

      <div>
        <Label>Min. Oda Sayısı</Label>
        <Input type="number" {...form.register('minBedrooms', { valueAsNumber: true })} />
      </div>

      <div>
        <Label>Min. m²</Label>
        <Input type="number" {...form.register('minSquareMeters', { valueAsNumber: true })} />
      </div>

      <Button type="submit" className="w-full">Filtrele</Button>
    </form>
  )
} 