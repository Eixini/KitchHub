<template>
    <div class="content">
        <ul id="recipeList">
            <li v-for="recipe in recipes">
                <button v-on:click="showDescription()">{{ recipe }}</button>
            </li>
        </ul>
        <div>
            <textarea id="recipeDescription" readonly>

            </textarea>
        </div>
    </div>
</template>

<script lang="js">

export default {

    name: 'ShowResultRecipes',

    data() {
    },
    methods:{

    }
}

</script>