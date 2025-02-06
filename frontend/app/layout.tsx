import { Inter } from 'next/font/google'
import { metadata } from './metadata'
import './globals.css'
import { Providers } from './providers'
import { Navbar } from '@/components/layout/Navbar'

const inter = Inter({ subsets: ['latin'] })

export { metadata }

export default function RootLayout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
    <html lang="tr">
      <body className={inter.className}>
        <Providers>
          <Navbar />
          <main className="min-h-screen bg-background">{children}</main>
        </Providers>
      </body>
    </html>
  )
} 