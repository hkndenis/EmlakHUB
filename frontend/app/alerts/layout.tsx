import { Metadata } from 'next'

export const metadata: Metadata = {
  title: 'Bildirimlerim | EmlakHUB',
  description: 'İlan bildirimlerinizi yönetin',
}

export default function AlertsLayout({
  children,
}: {
  children: React.ReactNode
}) {
  return children
} 