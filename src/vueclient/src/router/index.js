import { createRouter } from 'vue-router'

import Home from '@/components/Home.vue'
import EnteringIngredients from '@/components/EnteringIngredients.vue'
import ShowResultRecipes from '@/components/ShowResultRecipes.vue'
import Profile from '@/components/Profile.vue'
import Registration from '@/components/Registration.vue'
import Authorization from '@/components/Authorization.vue'
import CreateUserRecipe from '@/components/CreateUserRecipe.vue'
import ModerPanel from '@/components/privilege/ModerPanel.vue'
import AdminPanel from '@/components/privilege/AdminPanel.vue'


const routes = [
    {
        // Стартовая страница
        path: '/',
        component: Home
    },
    {
        // Страница для ввода ингредиентов
        path: '/enteringingredients',
        component: EnteringIngredients
    },
    {
        // Страница с показом рецептов, которые удовлетворяют введенным ингредиентам
        path: '/showresultrecipes',
        component: ShowResultRecipes
    },
    {
        // Страница для авторизации
        path: '/login',
        component: Authorization
    },
    {
        // Страница для рпегистрации
        path: '/register',
        component: Registration
    },
    {
        // Профиль пользователя
        path: '/profile',
        component: Profile
    },
    {
        // Страница для создания рецептов, доступ к которой имеют только авторизованные пользователи
        path: '/createrecipe',
        component: CreateUserRecipe
    },
    {
        // Панель управления для администраторов
        path: '/admin',
        name: 'admin',
        component: AdminPanel
    },
    {
        // Панель управления для модераторов и администраторов
        path: '/moder',
        name: 'moder',
        component: ModerPanel
    },
]

export default function(history) {
    return createRouter({
        history,
        routes
    })
}