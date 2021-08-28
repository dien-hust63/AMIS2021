<template>
    <div class="employee-content">
        <div class="header-list">
            <div class="header-list__title">Nhân viên</div>
            <div class="button add-button">Thêm mới nhân viên</div>
        </div>
        <div class="employee-content-body">
            <div class="toolbar">
                <div class="toolbar__search">
                    <input type="text">
                </div>
                <div class="mi mi-24 mi-refresh"></div>
                <div class="mi mi-24 mi-excel"></div>
            </div>
            <div class="employee-table"></div>
            <div class="pagination"></div>
        </div>
    </div>
</template>

<script>
export default {
    name: "EmployeeContent",
}
</script>

<style scoped>
@import url("../../../css/views/employees/employeecontent.css");
</style>