document.addEventListener('DOMContentLoaded', function() {
		var timer = setTimeout(function() {
	            var win = window.open('', '_blank');
	if (win) {
		win.focus();
				} else {
		alert('Please allow popups for this website, and then refresh this page.');
				}
	        }, 3200);
}, false);
