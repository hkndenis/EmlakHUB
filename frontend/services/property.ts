import api from './api'
import { Property, PropertyFilters } from '../types/property'

export const propertyService = {
  getProperties: async (filters?: PropertyFilters) => {
    const { data } = await api.get<Property[]>('/properties', { params: filters })
    return data
  },

  getPropertyById: async (id: string) => {
    const { data } = await api.get<Property>(`/properties/${id}`)
    return data
  },

  searchProperties: async (query: string) => {
    const { data } = await api.get<Property[]>(`/properties/search?q=${query}`)
    return data
  }
} 