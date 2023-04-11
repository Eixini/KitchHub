import { createApp } from 'vue'
import App from './App.vue'
import createRouter from '@/router/index.js'
import { createWebHistory } from 'vue-router'
import { store } from '@/store/index.js'

const router = createRouter(createWebHistory())

createApp(App)
.use(router)
.use(store)
.mount('#app')