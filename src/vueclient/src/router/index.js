import { createRouter } from 'vue-router'

import Home from '@/components/Home.vue'
import EnteringIngredients from '@/components/EnteringIngredients.vue'
import ShowResultRecipes from '@/components/ShowResultRecipes.vue'


const routes = [
    { path: '/', component: Home },
    { path: '/enteringingredients', component: EnteringIngredients },
    { path: '/showresultrecipes', component: ShowResultRecipes }
]

export default function(history) {
    return createRouter({
        history,
        routes
    })
}