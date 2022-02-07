tests: internal-runtime
	$(MAKE) -C TestSimpleWorker $@
	$(MAKE) -C TestLoadInternalRuntime $@

internal-runtime:
	$(MAKE) -C DummyInternalRuntime $@

.PHONY: tests internal-runtime
