'use client'

import Link from 'next/link'
import { usePathname } from 'next/navigation'
import { useSession, signOut } from 'next-auth/react'
import { Button } from '@/components/ui/button'

export function Navbar() {
  const pathname = usePathname()
  const { data: session, status } = useSession()

  return (
    <nav className="border-b">
      <div className="container mx-auto px-4 h-16 flex items-center justify-between">
        <div className="flex items-center space-x-8">
          <Link href="/" className="text-xl font-bold">
            EmlakHUB
          </Link>
          <Link href="/properties" className="hover:text-primary">
            İlanlar
          </Link>
          {session && (
            <>
              <Link href="/favorites" className="hover:text-primary">
                Favoriler
              </Link>
              <Link href="/alerts" className="hover:text-primary">
                Bildirimler
              </Link>
            </>
          )}
        </div>

        <div className="flex items-center space-x-4">
          {session ? (
            <>
              <span>Merhaba, {session.user.name}</span>
              <Button variant="outline" onClick={() => signOut()}>
                Çıkış Yap
              </Button>
            </>
          ) : (
            <>
              <Link href="/login">
                <Button variant="ghost">Giriş Yap</Button>
              </Link>
              <Link href="/register">
                <Button>Kayıt Ol</Button>
              </Link>
            </>
          )}
        </div>
      </div>
    </nav>
  )
} 