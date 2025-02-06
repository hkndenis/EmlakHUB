import api from './api'
import { LoginCredentials, RegisterData, User } from '../types/auth'

export const authService = {
  login: async (credentials: LoginCredentials) => {
    const { data } = await api.post<{token: string, user: User}>('/auth/login', credentials)
    return data
  },

  register: async (registerData: RegisterData) => {
    const { data } = await api.post<{token: string, user: User}>('/auth/register', registerData)
    return data
  },

  getCurrentUser: async () => {
    const { data } = await api.get<User>('/auth/me')
    return data
  }
} 