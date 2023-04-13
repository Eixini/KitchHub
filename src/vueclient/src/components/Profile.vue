<template>
    <div class="content">
        <h3>User Profile</h3>
        <div>
            <span> {{ currentUser.nickname }}</span>
        </div>
    </div>
</template>

<script lang="js">

export default {

    computed: {
    currentUser() {
      return this.$store.state.auth.user;
    }
  },

}

</script>

<style scoped>

</style>