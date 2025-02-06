import { type ClassValue, clsx } from "clsx"
import { twMerge } from "tailwind-merge"

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs))
}

export function formatMoney(amount: number, currency: string = 'TRY') {
  return new Intl.NumberFormat('tr-TR', {
    style: 'currency',
    currency: currency,
    minimumFractionDigits: 0,
    maximumFractionDigits: 0,
  }).format(amount)
}

export function getImageUrl(path: string | null | undefined) {
  if (!path) return '/placeholder-property.jpg' // Varsayılan resim

  if (path.startsWith('http')) {
    return path
  }

  return `${process.env.NEXT_PUBLIC_API_URL}/uploads/${path}`
}

// Kullanımı:
// <Image src={getImageUrl(property.images[0].url)} ... /> 