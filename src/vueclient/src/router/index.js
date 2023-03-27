import { createRouter } from 'vue-router'

import Home from '@/components/Home.vue'
import EnteringIngredients from '@/components/EnteringIngredients.vue'


const routes = [
    { path: '/', component: Home },
    { path: '/enteringingredients', component: EnteringIngredients }
]

export default function(history) {
    return createRouter({
        history,
        routes
    })
}