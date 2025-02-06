import { Metadata } from 'next'

export const metadata: Metadata = {
  title: 'EmlakHUB',
  description: 'Modern emlak platformu',
  metadataBase: new URL('http://localhost:3000'),
  keywords: ['emlak', 'konut', 'satılık', 'kiralık', 'daire', 'ev'],
  authors: [{ name: 'EmlakHUB Team' }],
  openGraph: {
    title: 'EmlakHUB',
    description: 'Modern emlak platformu',
    type: 'website',
    locale: 'tr_TR',
  }
} 