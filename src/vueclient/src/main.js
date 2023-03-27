import { createApp } from 'vue'
import App from './App.vue'
import createRouter from '@/router/index.js'
import { createWebHistory } from 'vue-router'

const router = createRouter(createWebHistory())

createApp(App).use(router).mount('#app')
