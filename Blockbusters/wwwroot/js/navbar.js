$(document).ready(function() {
	$('.aside .item').removeClass('active');
	var current = window.location.href.split("/")[3];
	if (current !== '') {
		$('.aside a[href="/' + current + '"]').addClass('active');
	} else {
		$('.aside a[href="/"]').addClass('active');
	}
});